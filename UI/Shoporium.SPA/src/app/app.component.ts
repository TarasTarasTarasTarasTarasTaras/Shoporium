import { Component, OnInit } from '@angular/core';
import { AccountService } from './modules/authentication/services/account.service';
import { Account } from './modules/authentication/models/account';
import { Router } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { environment } from 'src/environments/environment';

let apiUrl = environment.baseUrl;


const productCategories = gql`query GetProductCategories {
  productCategories {
    name
  }
}`;

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
    private http: HttpClient,
    private router: Router,
    private accountService: AccountService) {
      this.apollo.query({ query: productCategories }).subscribe(res => {
        this.productCategories = res.data;
      })
    }

  ngOnInit(): void {
    this.accountService.account$.subscribe(
      (res: Account) => {
        this.currentUserName = res.firstName;
      }
    );
  }

  authorize() {
    this.router.navigate(['account/login']);
  }

  register() {
    this.router.navigate(['account/register']);
  }

  logout() {
    this.accountService.logout();
    this.currentUserName = '';
  }

  msg?: string;
  progressbar:number=0

  uploadedFile = (files:any)=>{
    if(files.length===0){
      return;
    }

    let filecollection : File[] =files;
    const formData = new FormData();

    Array.from(filecollection).map((file,index)=>{
      return formData.append('file' + index, file, file.name)
    }); 

    this.http.post('https://localhost:7291/weatherforecast/upload',formData, {reportProgress: true, observe:'events'})
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
