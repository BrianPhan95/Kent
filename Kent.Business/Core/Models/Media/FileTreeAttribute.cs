﻿namespace Kent.Business.Core.Models.Media
{
    public class FileTreeAttribute
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public string Rel { get; set; }

        public string @Class { get; set; }

        public bool IsImage { get; set; }
    }
}