﻿namespace PancakeAuthBackend.DataTransferObjects {
    public class BatchDTO {
        public string Name { get; set; } = null!;
        public ICollection<string> Students { get; set; } = null!;
    }
}
