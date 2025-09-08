using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about the lyrics of a song.</para>
    /// <para xml:lang="vi">Thông tin về lời bài hát.</para>
    /// </summary>
    public class LyricData
    {
        [JsonConstructor]
        internal LyricData() { }

        /// <summary>
        /// <para xml:lang="en">Information about a sentence in the karaoke lyrics.</para>
        /// <para xml:lang="vi">Thông tin về một câu trong lời bài hát karaoke.</para>
        /// </summary>
        public class Sentence
        {
            [JsonConstructor]
            internal Sentence() { }

            [JsonInclude, JsonPropertyName("words")]
            internal List<Word> words { get; set; } = [];
            /// <summary>
            /// <para xml:lang="en">List of words in the sentence.</para>
            /// <para xml:lang="vi">Danh sách các từ trong câu.</para>
            /// </summary>
            [JsonIgnore]
            public ReadOnlyCollection<Word> Words => words.AsReadOnly();
        }

        /// <summary>
        /// <para xml:lang="en">Information about a word in a sentence in the karaoke lyrics.</para>
        /// <para xml:lang="vi">Thông tin về một từ trong một câu của lời bài hát karaoke.</para>
        /// </summary>
        public class Word
        {
            [JsonConstructor]
            internal Word() { }

            /// <summary>
            /// <para xml:lang="en">The start time of the word in milliseconds.</para>
            /// <para xml:lang="vi">Thời gian bắt đầu của từ (tính bằng mili giây).</para>
            /// </summary>
            [JsonInclude, JsonPropertyName("startTime")]
            public long StartTime { get; internal set; }

            /// <summary>
            /// <para xml:lang="en">The end time of the word in milliseconds.</para>
            /// <para xml:lang="vi">Thời gian kết thúc của từ (tính bằng mili giây).</para>
            /// </summary>
            [JsonInclude, JsonPropertyName("endTime")]
            public long EndTime { get; internal set; }

            /// <summary>
            /// <para xml:lang="en">The content of the word.</para>
            /// <para xml:lang="vi">Nội dung của từ.</para>
            /// </summary>
            [JsonInclude, JsonPropertyName("data")]
            public string Content { get; internal set; } = "";
        }

        [JsonInclude, JsonPropertyName("sentences")]
        internal List<Sentence> sentences { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of sentences in the karaoke lyrics.</para>
        /// <para xml:lang="vi">Danh sách các câu trong lời bài hát karaoke.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<Sentence> Sentences => sentences.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">The url to the file containing the synced lyrics.</para>
        /// <para xml:lang="vi">URL đến tệp chứa lời bài hát đồng bộ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("file")]
        public string File { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The synced lyrics of the song.</para>
        /// <para xml:lang="vi">Lời bài hát đồng bộ của bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public string SyncedLyrics { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Indicates whether video background is enabled.</para>
        /// <para xml:lang="vi">Cho biết nền video có được bật hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("enabledVideoBG")]
        public bool EnabledVideoBackground { get; internal set; }

        [JsonInclude, JsonPropertyName("defaultIBGUrls")]
        internal List<string> defaultBackgroundUrls { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of default background URLs.</para>
        /// <para xml:lang="vi">Danh sách các URL nền mặc định.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<string> DefaultBackgroundUrls => defaultBackgroundUrls.AsReadOnly();

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("BGMode")]
        public int BackgroundMode { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Get the karaoke lyrics in the A2 format.</para>
        /// <para xml:lang="vi">Lấy lời karaoke ở định dạng A2.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">The formatted lyrics.</para>
        /// <para xml:lang="vi">Lời bài hát đã được định dạng.</para>
        /// </returns>
        public string GetEnhancedLyrics()
        {
            //TODO: split words object containing multiple words into multiple word objects
            if (Sentences.Count == 0)
                return "";
            long lastSentenceTimestamp = 0;
            string enhancedLyrics = "";
            foreach (Sentence sentence in Sentences)
            {
                long firstWordTimestamp = sentence.Words.First().StartTime;
                string lyricSentence = "";
                long lastTimestamp = 0;
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
                if (sentence.Words.Count > 1)
                    lyricSentence += $"<{TimeSpan.FromMilliseconds(lastWordTimestamp):mm\\:ss\\.ff}>";
                if (firstWordTimestamp - lastSentenceTimestamp > 5000)
                    if (enhancedLyrics.Length == 0)
                        enhancedLyrics += $"[00:00.00]♪{Environment.NewLine}";
                    else
                        enhancedLyrics += $"[{TimeSpan.FromMilliseconds(lastSentenceTimestamp + 500):mm\\:ss\\.ff}]♪{Environment.NewLine}";
                lastSentenceTimestamp = lastWordTimestamp;
                enhancedLyrics += lyricSentence + Environment.NewLine;
            }
            enhancedLyrics += $"[{TimeSpan.FromMilliseconds(Sentences.Last().Words.Last().EndTime):mm\\:ss\\.ff}]♪";
            return enhancedLyrics;
        }

        /// <summary>
        /// <para xml:lang="en">Get the plain lyrics of the song.</para>
        /// <para xml:lang="vi">Lấy lời bài hát không định dạng.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">The plain lyrics.</para>
        /// <para xml:lang="vi">Lời bài hát không định dạng.</para>
        /// </returns>
        public string GetPlainLyrics()
        {
            if (string.IsNullOrEmpty(SyncedLyrics))
                return "";
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
    }
}
