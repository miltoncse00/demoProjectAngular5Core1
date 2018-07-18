import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Check } from '../shared/check.model';

@Injectable()
export class DemoService {
    myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }


    saveCheck(check: Check) {
        if (check.Id == '') {
            check.Id = '00000000-0000-0000-0000-000000000000';
        }
        return this._http.post(this.myAppUrl + "api/Checks", check);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}
