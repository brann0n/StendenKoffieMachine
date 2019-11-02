using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.Compression;

namespace KoffieMachineBot.Commands
{
	public class VoiceCommands : ModuleBase<SocketCommandContext>
	{
		//[Command("stream", RunMode = RunMode.Async), Summary("streams the servers audio out to the bot")]
		public async Task Stream()
		{
			var thisV = Context.Message.Author.Id;
			var connection = await Context.Guild.GetUser(thisV).VoiceChannel.ConnectAsync();
			int test = connection.Latency;
			try
			{
				await PlayAudioAsync(connection);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine($"- {ex.StackTrace}");
			}
			await connection.StopAsync();
		}


		private async Task PlayAudioAsync(IAudioClient client)
		{			
			var discord = client.CreatePCMStream(AudioApplication.Music);
			WasapiLoopbackCapture CaptureInstance = new WasapiLoopbackCapture(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice());
		
			CaptureInstance.DataAvailable += (s, a) =>
			{
				//step 1
				//var resampleStream = new AcmStream(WaveFormat.CreateCustomFormat(WaveFormatEncoding.IeeeFloat, 48000, 2, 384000, 8, 32), WaveFormat.CreateCustomFormat(WaveFormatEncoding.Pcm, 48000, 2, 16000, 8, 16));
				var resampleStream = new AcmStream(new WaveFormat(41000, 16, 2), new WaveFormat(4800, 16, 2)); //causes demonic screeching

				//step 2
				byte[] source = a.Buffer;
				Buffer.BlockCopy(source, 0, resampleStream.SourceBuffer, 0, a.BytesRecorded);

				//step 3
				int sourceBytesConverted = 0;
				var convertedBytes = resampleStream.Convert(source.Length, out sourceBytesConverted);
				if (sourceBytesConverted != source.Length)
				{
					Console.WriteLine("We didn't convert everything {0} bytes in, {1} bytes converted");
				}

				//step 4
				var converted = new byte[convertedBytes];
				Buffer.BlockCopy(resampleStream.DestBuffer, 0, converted, 0, convertedBytes);


				discord.Write(converted, 0, a.BytesRecorded);
			};

			CaptureInstance.RecordingStopped += (s, a) =>
			{
				Console.WriteLine("Stopped Recording!");
				CaptureInstance.Dispose();
				discord.Dispose();
			};
	
			CaptureInstance.StartRecording();
			
			await Task.Delay(5000);
			CaptureInstance.StopRecording();
			await Task.Delay(5000); 
		}
	}
}
