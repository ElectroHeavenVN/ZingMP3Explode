using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Songs
{
    /// <summary>
    /// Information about the lyrics of a song.
    /// </summary>
    public class LyricData
    {
        /// <summary>
        /// Information about a sentence in the enhanced lyrics.
        /// </summary>
        public class Sentence
        {
            /// <summary>
            /// List of words in the sentence.
            /// </summary>
            [JsonPropertyName("words")]
            public List<Word>? Words { get; set; }
        }

        /// <summary>
        /// Information about a word in a sentence in the enhanced lyrics.
        /// </summary>
        public class Word
        {
            /// <summary>
            /// The start time of the word in milliseconds.
            /// </summary>
            [JsonPropertyName("startTime")]
            public long StartTime { get; set; }

            /// <summary>
            /// The end time of the word in milliseconds.
            /// </summary>
            [JsonPropertyName("endTime")]
            public long EndTime { get; set; }

            /// <summary>
            /// The content of the word.
            /// </summary>
            [JsonPropertyName("data")]
            public string Content { get; set; }
        }

        /// <summary>
        /// List of sentences in the enhanced lyrics. If the song has no enhanced lyrics, this property will be null.
        /// </summary>
        [JsonPropertyName("sentences")]
        public List<Sentence>? Sentences { get; set; }

        /// <summary>
        /// The url to the file containing the synced lyrics. If the song has no synced lyrics, this property will be null.
        /// </summary>
        [JsonPropertyName("file")]
        public string? File { get; set; }

        /// <summary>
        /// The synced lyrics of the song. If the song has no synced lyrics, this property will be null.
        /// </summary>
        [JsonIgnore]
        public string? SyncedLyrics { get; set; }

        /// <summary>
        /// Get the karaoke lyrics in the enhanced lyrics format.
        /// </summary>
        /// <returns>The formatted lyrics.</returns>
        public string? GetEnhancedLyrics()
        {
            if (Sentences == null)
                return null;
            long lastSentenceTimestamp = 0;
            string enhancedLyrics = "";
            foreach (Sentence sentence in Sentences)
            {
                long firstWordTimestamp = sentence.Words.First().StartTime;
                string lyricSentence = "";
                long lastTimestamp = 0;
                if (sentence.Words == null)
                    continue;
                foreach (Word word in sentence.Words)
                {
                    long timestamp = word.StartTime;
                    if (timestamp != lastTimestamp)
                    {
                        if (string.IsNullOrEmpty(lyricSentence))
                            lyricSentence += $"[{TimeSpan.FromMilliseconds(timestamp):mm\\:ss\\.ff}]";
                        else
                            lyricSentence += $"<{TimeSpan.FromMilliseconds(timestamp):mm\\:ss\\.ff}>";
                        lastTimestamp = timestamp;
                    }
                    lyricSentence += word.Content + ' ';
                }
                lyricSentence = lyricSentence.Trim();
                long lastWordTimestamp = sentence.Words.Last().EndTime;
                lyricSentence += $"<{TimeSpan.FromMilliseconds(lastWordTimestamp):mm\\:ss\\.ff}>";
                if (firstWordTimestamp - lastSentenceTimestamp > 5000)
                    enhancedLyrics += $"[{TimeSpan.FromMilliseconds(lastSentenceTimestamp + 500):mm\\:ss\\.ff}]♪{Environment.NewLine}";
                lastSentenceTimestamp = lastWordTimestamp;
                enhancedLyrics += lyricSentence + Environment.NewLine;
            }
            enhancedLyrics += $"[{TimeSpan.FromMilliseconds(Sentences.Last().Words.Last().EndTime):mm\\:ss\\.ff}]♪";
            return enhancedLyrics;
        }

        /// <summary>
        /// Get the plain lyrics of the song.
        /// </summary>
        /// <returns>The plain lyrics.</returns>
        public string? GetPlainLyrics()
        {
            if (SyncedLyrics == null)
                return null;
            string result = "";
            foreach (string sentence in SyncedLyrics.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                string lyric = sentence;
                if (sentence.Contains(']'))
                    lyric = sentence.Remove(sentence.IndexOf('['), sentence.LastIndexOf(']') - sentence.IndexOf('[') + 1).Trim();
                result += lyric + Environment.NewLine;
            }
            result = result.Trim(Environment.NewLine.ToCharArray());
            return result;
        }

#pragma warning disable CS1591 //Unknown JSON properties
        [JsonPropertyName("enabledVideoBG")]
        public bool EnabledVideoBackground { get; set; }
        [JsonPropertyName("defaultIBGUrls")]
        public List<string>? DefaultBackgroundUrls { get; set; }
        [JsonPropertyName("BGMode")]
        public int BackgroundMode { get; set; }
#pragma warning restore CS1591 
    }
}
