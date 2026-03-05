export interface TeamResponseDto {
    id: string;
    teamName: string;
    shortName: string;
    role: string;
    status: "Pending" | "Active" | "Declined";
}

export interface TeamMemberDto {
    id: string;
    email: string;
    role: "Owner" | "Coach" | "Member";
    isActive: boolean;
}

export interface TeamDetailDto {
    id: string;
    teamName: string;
    shortName: string;
    description?: string;
    myRole: "Owner" | "Coach" | "Member";
    members: TeamMemberDto[];
}

export interface PendingInvitationDto {
    teamId: string;
    teamName: string;
    invitingRole: string;
    invitedAt: string;
}