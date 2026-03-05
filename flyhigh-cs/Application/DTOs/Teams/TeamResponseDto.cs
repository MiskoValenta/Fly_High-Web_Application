using Domain.Entities.Teams.TeamEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Teams;

public record TeamResponseDto(Guid Id, string TeamName, string ShortName, string Role, string Status);
