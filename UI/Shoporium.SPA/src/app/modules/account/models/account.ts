import { Role } from "./role";

export interface Account {
    userId: number;
    firstName: string;
    lastName: string;
    role: Role;
    email: string;
    mobileNumber: string;
}
