using NAudio.Wave;
using System.Buffers.Text;

namespace BLL
{
    public class TrackService : ITrackService
    {
        private IUnityOfWork context;

        public TrackService(IUnityOfWork context)
        {
            this.context = context;
        }

        public void AddTrack(Track track)
        {
            AudioData audioData = new AudioData(track.Fingerprint);
            WaveFormatBuilder waveFormatBuilder = new WaveFormatBuilder();
            waveFormatBuilder.SetChannels(1);
            waveFormatBuilder.SetBitsPerSample(16);
            waveFormatBuilder.SetSampleRate(16000);
            WaveFormat waveFormat = waveFormatBuilder.GetWaveFormat();

            audioData.ChangeAudioFormat(waveFormat);

            SamplesExtractor samplesExtractor = new SamplesExtractor(audioData);

            CompactMfccExtractor mfccExtractor = new CompactMfccExtractor();

            byte[] fingerprint = mfccExtractor.ComputeFrom(waveFormat, samplesExtractor.samples);
            track.Fingerprint = fingerprint;

            context.Tracks.Create(track);
            context.Save();
        }

        public void DeleteTrack(int id)
        {
            context.Tracks.Delete(id);
        }

        public List<Genre> GetGenres()
        {
            return context.Genres.GetList().ToList();
        }

        public Track GetLastReleaseOfArtist(int artistId)
        {
            return context.Tracks.GetList().Where(i => i.ArtistId == artistId).OrderByDescending(i => i.PublishDate).FirstOrDefault();
        }

        public Track GetTrack(int id)
        {
            return context.Tracks.GetItem(id);
        }

        public List<Track> GetTracks()
        {
            return context.Tracks.GetList();
        }

        public List<Track> GetTracksMatchedString(string expression)
        {
            return context.Tracks.GetList().Where(i => i.Title.ToLower().Contains(expression.ToLower())).ToList();
        }

        public List<Track> GetTracksOfAlbum(int albumId)
        {
            return context.Tracks.GetList().Where(i => i.AlbumId == albumId).ToList();
        }

        public List<Track> GetTracksOfArtist(int artistId)
        {
            return context.Tracks.GetList().Where(i => i.ArtistId == artistId).ToList();
        }

        public List<Tuple<Track, int>> GetTracksOfPlaylist(int playlistId)
        {
            var playlistPositions = context.PlaylistPositions.GetList().Where(i => i.PlaylistId == playlistId).ToList();

            List<Tuple<Track, int>> tracks = new List<Tuple<Track, int>>();
            foreach (var position in playlistPositions)
            {
                Track track = context.Tracks.GetItem(position.TrackId);
                tracks.Add(new Tuple<Track, int>(track, position.Id));
            }
            return tracks;
        }

        public void UpdateTrack(Track track)
        {            
            context.Tracks.Update(track);
            context.Save();
        }
    }
}
