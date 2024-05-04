export interface ProductModel {
    id: number;
    name: string;
    description: string;
    price: number;
    status: number;
    numberOfViews: number;
    createdDate: Date;
    productPhotos: string[];
    downloadedPhotos: (Uint8Array | null)[];
    cityId?: number;
    condition: number;
    categoryId: number;
    storeId: number;
    // store?: Store;
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
  