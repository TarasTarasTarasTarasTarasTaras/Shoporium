import { Role } from "./role";

export interface LoginResult {
    id: number;
    firstName?: string;
    lastName?: string;
    role: Role;
    email?: string;
    mobileNumber: string;
    accessToken: string;
    refreshToken: string;
  }