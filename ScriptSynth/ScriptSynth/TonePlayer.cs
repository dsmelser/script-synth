using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptSynth
{
    public class TonePlayer
    {
        // The sample rate and bit depth are the two parameters that determine the
        // quality of the audio

        //we'll start them with some default wav values
        private readonly ulong _samplesPerSecond = 44100;
        private readonly ulong _bitsPerSample = 16;
        private readonly ulong _bytesPerSample = 2;

        public TonePlayer(ulong samplesPerSecond, ulong bitsPerSample)
        {
            _samplesPerSecond = samplesPerSecond;
            _bitsPerSample = bitsPerSample;
            _bytesPerSample = bitsPerSample / 8;
        }

        public void Play(List<ToneGenerator> toneGenerators)
        {
            double[] audioBuffer = ToneCombiner.GenerateAudioBuffer(_samplesPerSecond, toneGenerators);


            int audioDataSize = (int)((ulong)audioBuffer.Length * _bytesPerSample);

            // Convert the audio buffer to a byte array
            byte[] audioBytes = new byte[audioDataSize];
            for (ulong i = 0; i < (ulong)audioBuffer.Length; i++)
            {
                short sample = (short)(audioBuffer[i] * 32767.0);
                byte[] sampleBytes = BitConverter.GetBytes(sample);
                Array.Copy(sampleBytes, 0, audioBytes, (int)(i * _bytesPerSample), (int)_bytesPerSample);
            }

            // Play the audio using a SoundPlayer
            using (MemoryStream audioStream = new MemoryStream(audioDataSize + 44))
            {
                bool littleEndian = BitConverter.IsLittleEndian;

                if (!littleEndian) throw new Exception("uh oh, big endian");

                //write the wav header tp the memory stream
                audioStream.Position = 0;
                audioStream.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);

                //bytes 5-8 are the file size
                //file size is the header size + the size of the audio data
                //the header size is 44 bytes

                //the audio data size is the number of samples *
                //the number of channels *
                //the number of bytes per sample

                //I think we only have one channel for this use case

                //so the audio data size is the number of samples * the number of bytes per sample


                int totalDataSize = audioDataSize + 44 - 8; //we count starting from 8

                int channels = 1; //mono
                
                //convert int to byte array

                byte[] totalDataSizeBytes = BitConverter.GetBytes(totalDataSize);

                audioStream.Write(totalDataSizeBytes, 0, 4);

                //now we need to write the word WAVE to the stream
                audioStream.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);

                //now we need to write the word fmt to the stream with a trailing space
                audioStream.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);

                //now we need to write the size of the fmt chunk to the stream
                //which is 16 bytes
                audioStream.Write(new byte[] { 0x10, 0x00, 0x00, 0x00 }, 0, 4);

                //now we need to write the audio format to the stream
                //which is 1 for PCM
                audioStream.Write(new byte[] { 0x01, 0x00 }, 0, 2);

                //now we need to write the number of channels to the stream
                //which is 1 for mono
                audioStream.Write(new byte[] { (byte)channels, 0x00 }, 0, 2);

                //now we need to write the sample rate to the stream
                audioStream.Write(BitConverter.GetBytes((int)_samplesPerSecond), 0, 4);

                //now we need to write the byte rate to the stream
                //which is the sample rate * the number of channels * the number of bytes per sample
                audioStream.Write(BitConverter.GetBytes((int)_samplesPerSecond * channels * (int)_bytesPerSample), 0, 4);

                //now we need to write the number of bytes per sample
                audioStream.Write(BitConverter.GetBytes((short)((int)_bytesPerSample * channels)), 0, 2);

                //now we need to write the number of bits per sample
                audioStream.Write(BitConverter.GetBytes((short)_bitsPerSample), 0, 2);

                //now we need to write the word data to the stream
                audioStream.Write(Encoding.ASCII.GetBytes("data"), 0, 4);

                //now we need to write the size of the audio data to the stream
                audioStream.Write(BitConverter.GetBytes(audioDataSize), 0, 4);

               

                //now we need to write the audio data to the stream
                audioStream.Write(audioBytes, 0, audioDataSize);


                //TestAudioStream(audioStream);

                audioStream.Position = 0;

                using (SoundPlayer soundPlayer = new SoundPlayer(audioStream))
                {
                    soundPlayer.PlaySync();
                    //soundPlayer.Stop();
                }

            }
        }

        private void TestAudioStream(MemoryStream audioStream)
        {
            //step one, get all the bytes as an array

            int length = (int)audioStream.Length;
            audioStream.Position = 0;
            byte[] data = new byte[length];
            audioStream.Read(data, 0, length);

            //write the bytes to a wav file
            //File.WriteAllBytes("test.wav", data);

            int num = 0;
            short num2 = -1;
            bool flag = false;
            if (data.Length < 12)
            {
                throw new Exception("PROBLEM");
            }

            if (data[0] != 82 || data[1] != 73 || data[2] != 70 || data[3] != 70)
            {
                throw new Exception("PROBLEM");
            }

            if (data[8] != 87 || data[9] != 65 || data[10] != 86 || data[11] != 69)
            {
                throw new Exception("PROBLEM");
            }

            num = 12;
            int num3 = data.Length;
            while (!flag && num < num3 - 8)
            {
                if (data[num] == 102 && data[num + 1] == 109 && data[num + 2] == 116 && data[num + 3] == 32)
                {
                    flag = true;
                    int num4 = BytesToInt(data[num + 7], data[num + 6], data[num + 5], data[num + 4]);
                    int num5 = 16;
                    if (num4 != num5)
                    {
                        int num6 = 18;
                        if (num3 < num + 8 + num6 - 1)
                        {
                            throw new Exception("PROBLEM");
                        }

                        short num7 = BytesToInt16(data[num + 8 + num6 - 1], data[num + 8 + num6 - 2]);
                        if (num7 + num6 != num4)
                        {
                            throw new Exception("PROBLEM");
                        }
                    }

                    if (num3 < num + 9)
                    {
                        throw new Exception("PROBLEM");
                    }

                    num2 = BytesToInt16(data[num + 9], data[num + 8]);
                    num += num4 + 8;
                }
                else
                {
                    num += 8 + BytesToInt(data[num + 7], data[num + 6], data[num + 5], data[num + 4]);
                }
            }

            if (!flag)
            {
                throw new Exception("PROBLEM");
            }

            if (num2 != 1 && num2 != 2 && num2 != 3)
            {
                throw new Exception("format not supported");
            }


            audioStream.Position = 0;
        }

        private static short BytesToInt16(byte ch0, byte ch1)
        {
            int num = ch1;
            num |= ch0 << 8;
            return (short)num;
        }

        private static int BytesToInt(byte ch0, byte ch1, byte ch2, byte ch3)
        {
            return mmioFOURCC((char)ch3, (char)ch2, (char)ch1, (char)ch0);
        }

        private static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
        {
            int num = 0;
            num |= ch0;
            num |= (int)((uint)ch1 << 8);
            num |= (int)((uint)ch2 << 16);
            return num | (int)((uint)ch3 << 24);
        }
    }
}
