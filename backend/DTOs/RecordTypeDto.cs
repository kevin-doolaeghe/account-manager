﻿using backend.Models;

namespace backend.DTOs {

    public class RecordTypeGetDto {

        public long RecordTypeId { get; set; }

        public string? Name { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }

        public static RecordTypeGetDto ToDto(RecordType item) {
            return new RecordTypeGetDto {
                RecordTypeId = item.RecordTypeId,
                Name = item.Name,
                Icon = item.Icon,
                Color = item.Color,
            };
        }
    }

    public class RecordTypePostDto {

        public string? Name { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }

        public static RecordType ToItem(RecordTypePostDto dto) {
            return new RecordType {
                Name = dto.Name,
                Icon = dto.Icon,
                Color = dto.Color,
            };
        }
    }
}