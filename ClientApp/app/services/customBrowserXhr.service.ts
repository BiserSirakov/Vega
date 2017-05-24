import { Injectable } from '@angular/core';
import { BrowserXhr } from '@angular/http';

import { ProgressService } from './progress.service';

@Injectable()

export class CustomBrowserXhr extends BrowserXhr {

    constructor(private service: ProgressService) {
        super();
    }

    build(): XMLHttpRequest {
        var xhr: XMLHttpRequest = super.build();

        xhr.onprogress = (event) => {
            this.service.notify(this.createProgress(event));
        };

        xhr.upload.onprogress = (event) => {
            this.service.notify(this.createProgress(event));
        };

        xhr.upload.onloadend = () => {
            console.log("BEFORE", this.service.startTracking());
            this.service.endTracking();
            console.log("AFTER", this.service.startTracking());
        };

        return xhr;
    }

    private createProgress(event) {
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        };
    }
}