namespace MusicHub
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.Data.SqlClient.Server;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            string result = ExportSongsAboveDuration(context, 120);

            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var albums = context.Producers
                .FirstOrDefault(p => p.Id == producerId)
            .Albums
            .Select(a => new
            {
                AlbumName = a.Name,
                AlbumPrice = a.Price,
                ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                ProducerName = a.Producer.Name,
                AlbumSongs = a.Songs.Select(s => new
                {
                    SongName = s.Name,
                    SongPrice = s.Price,
                    SongWriterName = s.Writer.Name,
                })
                .OrderByDescending(s => s.SongName)
                .ThenBy(s => s.SongWriterName)
            })
            .OrderByDescending(a => a.AlbumPrice)
            .ToList();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");

                int songCouner = 1;

                foreach (var song in album.AlbumSongs)
                {
                    
                    sb.AppendLine($"---#{songCouner}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.SongPrice:f2}");
                    sb.AppendLine($"---Writer: {song.SongWriterName}");

                    songCouner++;
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();

            var songs = context.Songs
                .AsEnumerable()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    s.Name,
                    Performers = s.SongPerformers
                    .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                    .OrderBy(p => p)
                    .ToArray(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album!.Producer!.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.WriterName)
                .ToArray();

            int songCounter = 1;

            foreach (var song in songs)
            {               
                sb.AppendLine($"-Song #{songCounter}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");

                foreach (var performer in song.Performers)
                {
                    sb.AppendLine($"---Performer: {performer}");
                }
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");

                songCounter++;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
