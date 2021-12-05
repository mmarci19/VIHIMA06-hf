import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-upload',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.css'],
})
export class UploadComponent {
    progress: number = 0;
    message: string = '';
    @Output() public onUploadFinished = new EventEmitter();

    constructor(private http: HttpClient) { }

    public uploadFile = (files: FileList | null) => {
        if (files === null || files.length === 0) {
            return;
        }
        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        this.message = '';
        this.http
            .post('https://localhost:5001/api/store/upload', formData, {
                reportProgress: true,
                observe: 'events',
            })
            .subscribe((event) => {
                if (event.type === HttpEventType.UploadProgress)
                    this.progress = Math.round((100 * event.loaded) / (event.total ?? 1));
                else {
                    if (event.type === HttpEventType.Response && event.ok) {
                        this.message = 'Sikeres feltöltés';
                        this.onUploadFinished.emit();
                    }
                    else if (event.type === HttpEventType.ResponseHeader && event.ok === false) {
                        this.message = 'A fájl feltöltése során hiba történt.';
                    }
                    this.progress = 0;
                }
            });
    };
}
