export class ProductModel {
    id: number;
    name: string;
    description: string;
    price: number;
    status: number;
    numberOfViews: number;
    createdDate: Date;
    productPhotos;
    downloadedPhotos: (Uint8Array | null)[];
    cityId?: number;
    condition: number;
    categoryId: number;
    storeId: number;
    // store?: Store;

    constructor(id: number, name: string, description: string, categoryId: number, price:number, productPhotos: string[]) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.categoryId = categoryId;
        this.price = price;
        this.productPhotos = productPhotos;
    }
  }
  
//   export enum ProductStatus {
//     Active = 'Active',
//     Inactive = 'Inactive',
//     Pending = 'Pending',
//   }
  
//   export enum ProductCondition {
//     New = 'New',
//     Used = 'Used',
//   }
  