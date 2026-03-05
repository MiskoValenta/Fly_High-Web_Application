export enum MatchStatus {
    Proposed = 0,
    Accepted = 1,
    Rejected = 2,
    Scheduled = 3,
    InProgress = 4,
    Finished = 5,
    Cancelled = 6
}

export enum SetType {
    Normal = 0,
    TieBreak = 1
}

export enum SetSide {
    Home = 0,
    Away = 1
}

export enum SetWinner {
    None = 0,
    Home = 1,
    Away = 2
}

export enum PlayerPosition {
    Setter = 0,
    OppositeHitter = 1,
    Blocker = 2,
    OutsideHitter = 3,
    Libero = 4,
    Bench = 5
}

export interface MatchPlayerPosition {
    id: string;
    teamMemberId: string;
    position: PlayerPosition;
}

export interface MatchSet {
    id: string;
    setNumber: number;
    type: SetType;
    homeScore: number;
    awayScore: number;
    isFinished: boolean;
    winner: SetWinner;
    playerPositions: MatchPlayerPosition[];
}

export interface MatchRosterEntry {
    id: string;
    matchId: string;
    teamMemberId: string;
    teamId: string;
    jerseyNumber: number;
}

export interface Match {
    id: string;
    homeTeamId: string;
    awayTeamId: string;
    location: string;
    scheduledAt: string;
    refereeId: string | null;
    notes: string | null;
    status: MatchStatus;
    sets: MatchSet[];
    roster: MatchRosterEntry[];
}