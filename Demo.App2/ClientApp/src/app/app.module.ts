import { BrowserModule } from '@angular/platform-browser';
import { NgModule, forwardRef, Input } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DemoService } from './../shared/demoService';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CheckComponent } from './check/check.component';
import { ModalModule } from 'ngx-bootstrap/modal';


import { isNumber, toInteger } from '@ng-bootstrap/ng-bootstrap/util/util';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CheckComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: CheckComponent, pathMatch: 'full' },
      { path: 'check-entry', component: CheckComponent }
    ])
  ],
  providers: [
    DemoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

