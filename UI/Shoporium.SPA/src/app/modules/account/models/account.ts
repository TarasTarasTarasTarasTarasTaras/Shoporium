import { Role } from "./role";

export interface Account {
    userId: number;
    healthCardId: number;
    firstName: string;
    lastName: string;
    role: Role;
    email: string;
}
