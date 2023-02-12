﻿namespace backend.Models {

    public class Record {

        public int Id { get; set; }

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public RecordType? Type { get; set; }
    }
}