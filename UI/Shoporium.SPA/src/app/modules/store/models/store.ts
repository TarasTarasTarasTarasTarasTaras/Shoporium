import { StoreStatus } from "./store-status";

export interface Store {
    name: string;
    description: string;
    mainPhoto: string;
    backgroundPhoto: string;
    otherCategoryName: string | null;
    // status: StoreStatus;
    // userId: number;
    // categoryId: number;
}