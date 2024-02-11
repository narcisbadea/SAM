// app.module.ts

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppService } from './app.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip'; // Import MatTooltipModule
import { AppTable } from './app-table/app-table.component';

@NgModule({
  declarations: [AppComponent, AppTable],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatTableModule,
    MatTooltipModule,
  ],
  providers: [AppService],
  bootstrap: [AppComponent],
})
export class AppModule {}
