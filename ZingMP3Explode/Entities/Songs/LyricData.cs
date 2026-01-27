using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

            /// <inheritdoc/>
            public override string ToString() => string.Join(" ", words.Select(w => w.Content));
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

            /// <inheritdoc/>
            public override string ToString() => Content;
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
        /// <param name="splitWords">
        /// <para xml:lang="en">Whether to split <see cref="Word"/> objects containing multiple words into multiple words with evenly distributed timestamps.</para>
        /// <para xml:lang="vi">Tách các <see cref="Word"/> chứa nhiều từ thành nhiều từ với dấu thời gian chia đều hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The formatted lyrics.</para>
        /// <para xml:lang="vi">Lời bài hát đã được định dạng.</para>
        /// </returns>
        public string GetEnhancedLyrics(bool splitWords = false)
        {
            if (Sentences.Count == 0)
                return "";
            long lastSentenceTimestamp = 0;
            StringBuilder enhancedLyrics = new StringBuilder();
            StringBuilder lyricSentence = new StringBuilder();
            List<List<Word>> sentenceWords = Sentences.Select(s => s.words).ToList();
            foreach (List<Word> w in sentenceWords)
            {
                List<Word> words = [..w];
                long firstWordTimestamp = words[0].StartTime;
                lyricSentence.Clear();
                long lastTimestamp = 0;
                if (splitWords)
                {
                    int sameTimeWordsCount = 0;
                    for (int i = 0; i < words.Count; i++)
                    {
                        var currentWord = words[i];
                        if (currentWord.StartTime == currentWord.EndTime)
                            sameTimeWordsCount++;
                        else if (sameTimeWordsCount > 0)
                        {
                            long timePerWord = (currentWord.EndTime - currentWord.StartTime) / (sameTimeWordsCount + 1);
                            for (int j = 0; j < sameTimeWordsCount; j++)
                            {
                                Word word = new Word
                                {
                                    Content = words[i - j - 1].Content,
                                    StartTime = words[i - j - 1].StartTime + timePerWord * j
                                };
                                word.EndTime = word.StartTime + timePerWord;
                                words[i - j - 1] = word;
                            }
                            words[i] = new Word
                            {
                                Content = currentWord.Content,
                                StartTime = words[i - 1].EndTime,
                                EndTime = currentWord.EndTime
                            };
                            sameTimeWordsCount = 0;
                        }
                    }
                }
                for (int i = 0; i < words.Count; i++)
                {
                    long timestamp = words[i].StartTime;
                    if (timestamp != lastTimestamp)
                    {
                        if (lyricSentence.Length == 0)
                            lyricSentence.Append($"[{TimeSpan.FromMilliseconds(timestamp):mm\\:ss\\.ff}] ");
                        else
                            lyricSentence.Append($"<{TimeSpan.FromMilliseconds(timestamp):mm\\:ss\\.ff}>");
                        lastTimestamp = timestamp;
                    }
                    lyricSentence.Append(words[i].Content);
                    if (i < words.Count - 1)
                        lyricSentence.Append(' ');
                }
                long lastWordTimestamp = words[words.Count - 1].EndTime;
                if (words.Count > 1)
                    lyricSentence.Append($"<{TimeSpan.FromMilliseconds(lastWordTimestamp):mm\\:ss\\.ff}>");
                if (firstWordTimestamp - lastSentenceTimestamp > 5000)
                {
                    if (enhancedLyrics.Length == 0)
                        enhancedLyrics.AppendLine($"[00:00.00] ♪");
                    else
                        enhancedLyrics.AppendLine($"[{TimeSpan.FromMilliseconds(lastSentenceTimestamp + 500):mm\\:ss\\.ff}] ♪");
                }
                lastSentenceTimestamp = lastWordTimestamp;
                enhancedLyrics.AppendLine(lyricSentence.ToString().Trim());
            }
            var lastSentenceWords = sentenceWords[sentenceWords.Count - 1];
            enhancedLyrics.Append($"[{TimeSpan.FromMilliseconds(lastSentenceWords[lastSentenceWords.Count - 1].EndTime + 2000):mm\\:ss\\.ff}] ♪");
            return enhancedLyrics.ToString();
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
            StringBuilder result = new StringBuilder();
            foreach (string sentence in SyncedLyrics.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                string lyric = sentence;
                if (sentence.Contains(']'))
                    lyric = sentence.Remove(sentence.IndexOf('['), sentence.LastIndexOf(']') - sentence.IndexOf('[') + 1).Trim();
                result.AppendLine(lyric);
            }
            return result.ToString().Trim(Environment.NewLine.ToCharArray());
        }
    }
}
