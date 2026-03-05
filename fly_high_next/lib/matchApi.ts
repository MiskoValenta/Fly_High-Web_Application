// lib/matchApi.ts
import { Match, SetSide, PlayerPosition } from '../types/match';
import { fetchWithAuth } from './apiClient';

export const getMatches = async (): Promise<Match[]> => {
    const response = await fetchWithAuth('/Match');
    if (!response.ok) throw new Error('Failed to fetch matches');
    return response.json();
};

export const getMatchById = async (id: string): Promise<Match> => {
    const response = await fetchWithAuth(`/Match/${id}`);
    if (!response.ok) throw new Error('Failed to fetch match');
    return response.json();
};

export const proposeMatch = async (data: { homeTeamId: string, awayTeamId: string, scheduledAt: string, location: string }) => {
    const response = await fetchWithAuth('/Match/propose', {
        method: 'POST',
        body: JSON.stringify(data)
    });
    if (!response.ok) throw new Error('Failed to propose match');
    return response.json();
};

export const acceptMatch = async (id: string) => {
    await fetchWithAuth(`/Match/${id}/accept`, { method: 'POST' });
};

export const rejectMatch = async (id: string) => {
    await fetchWithAuth(`/Match/${id}/reject`, { method: 'POST' });
};

export const startMatch = async (id: string) => {
    await fetchWithAuth(`/Match/${id}/start`, { method: 'POST' });
};

export const addPoint = async (id: string, side: SetSide) => {
    await fetchWithAuth(`/Match/${id}/point/${side}`, { method: 'POST' });
};

export const addToRoster = async (id: string, data: { teamMemberId: string, teamId: string, jerseyNumber: number }) => {
    await fetchWithAuth(`/Match/${id}/roster`, {
        method: 'POST',
        body: JSON.stringify(data)
    });
};

export const assignPosition = async (id: string, data: { setNumber: number, teamMemberId: string, position: PlayerPosition }) => {
    await fetchWithAuth(`/Match/${id}/positions`, {
        method: 'POST',
        body: JSON.stringify(data)
    });
};