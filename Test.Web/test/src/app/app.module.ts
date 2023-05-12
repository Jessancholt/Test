import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {AppDataService} from "./app.data.service";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent
  ],
    imports: [BrowserModule, HttpClientModule, FormsModule],
  providers: [AppDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
