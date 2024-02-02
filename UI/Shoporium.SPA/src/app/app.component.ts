import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const productCategoriesQuery = gql`
  query GetProductCategories {
  productCategories(where: { iconName: {neq: ""} }) {
    name,
    iconName
  }
}
`;


// const productCategories = gql`query GetProductCategories {
//   productCategories {
//     name,
//     iconName
//   }
// }`;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Shoporium.SPA';

  currentUserName: string;
  productCategories;

  constructor(
    private apollo: Apollo,
    private http: HttpClient) {
      this.apollo.query({ query: productCategoriesQuery }).subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
        console.log(res)
      })
    }

  ngOnInit(): void {
  }

  msg?: string;
  progressbar:number=0

  uploadedFile = (files:any)=>{
    if(files.length===0){
      return;
    }

    let filecollection : File[] = files;
    const formData = new FormData();

    Array.from(filecollection).map((file,index)=>{
      return formData.append('file' + index, file, file.name)
    }); 

    this.http.post(`${environment.baseUrl}weatherforecast/upload`,formData, {reportProgress: true, observe:'events'})
      .subscribe({
        next:(event)=>{
          if(event.type === HttpEventType.UploadProgress) {  
            if(event?.loaded && event?.total) {
              this.progressbar = Math.round(100 * event.loaded / event.total)
            }
          }
          else if (event.type === HttpEventType.Response) {
            this.msg = 'Upload successfully completed..!';
          }
        },
        error: (err:HttpErrorResponse)=> console.log(err)
      });
  }
}
