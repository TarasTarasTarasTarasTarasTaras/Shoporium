import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Shoporium.SPA';

  currentUserName: string;
  
  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {}

  msg?: string;
  progressbar: number = 0;

  uploadedFile = (files: any) => {
    if (files.length === 0) {
      return;
    }

    let filecollection: File[] = files;
    const formData = new FormData();

    Array.from(filecollection).map((file, index) => {
      return formData.append('file' + index, file, file.name);
    });

    this.http
      .post(`${environment.baseUrl}weatherforecast/upload`, formData, {
        reportProgress: true,
        observe: 'events',
      })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress) {
            if (event?.loaded && event?.total) {
              this.progressbar = Math.round((100 * event.loaded) / event.total);
            }
          } else if (event.type === HttpEventType.Response) {
            this.msg = 'Upload successfully completed..!';
          }
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  };
}
