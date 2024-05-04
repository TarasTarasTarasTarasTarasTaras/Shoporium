export class Store {
    id: number;
    name: string;
    description: string;

    mainPhoto: string;
    backgroundPhoto: string;

    categoryId: number;
    otherCategoryName: string | null;

    userId: number;

    downloadedMainPhoto;
    downloadedBackgroundPhoto;
    // status: StoreStatus;
    // userId: number;
    // categoryId: number;

    constructor(id: number, name: string, description: string, mainPhoto, backgroundPhoto, categoryId: number, otherCategoryName: string | null) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.mainPhoto = mainPhoto;
        this.backgroundPhoto = backgroundPhoto;
        this.categoryId = categoryId;
        this.otherCategoryName = otherCategoryName;
    }
}