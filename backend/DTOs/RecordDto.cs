﻿using backend.Models;

namespace backend.DTOs {

    public class RecordGetDto {

        public long RecordId { get; set; }

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public long RecordTypeId { get; set; }

        public long AccountId { get; set; }

        public static RecordGetDto ToDto(Record item) {
            return new RecordGetDto {
                RecordId = item.RecordId,
                Description = item.Description,
                Amount = item.Amount,
                Status = item.Status,
                Date = item.Date,
                RecordTypeId = item.RecordTypeId,
                AccountId = item.AccountId,
            };
        }
    }

    public class RecordPostDto {

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public long RecordTypeId { get; set; }

        public long AccountId { get; set; }

        public static Record ToItem(RecordPostDto dto) {
            return new Record {
                Description = dto.Description,
                Amount = dto.Amount,
                Status = dto.Status,
                Date = dto.Date,
                RecordTypeId = dto.RecordTypeId,
                AccountId = dto.AccountId,
            };
        }
    }
}
