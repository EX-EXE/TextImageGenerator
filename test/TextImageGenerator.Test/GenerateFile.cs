namespace TextImageGenerator.Test
{
    public class GenerateFile
    {
        [Fact]
        public void OutputPng4K()
        {
            var content = new TextImageContent()
            {
                ImageWidth = 3840,
                ImageHeight = 2160,
                EncodeFormat = "png",
                Lines = new[]
                {
                    new TextImageLineText(){ Text = "‚ ‚¢‚¤‚¦‚¨",FontFamily="Meiryo"},
                    new TextImageLineText(){ Text = "ABCDE"},
                    new TextImageLineText(){ Text = "0123456789"},
                }
            };
            var tmpPath = System.IO.Path.GetTempFileName();
            TextImageGenerator.GenerateFile(tmpPath, content);
            Assert.True(0 < new System.IO.FileInfo(tmpPath).Length);
        }
    }
}