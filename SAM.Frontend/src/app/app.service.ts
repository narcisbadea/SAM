// app.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  private apiUrlFree = 'https://localhost:7253/api/top-selling/free?number=';
  private apiUrlPaid = 'https://localhost:7253/api/top-selling/paid?number=';

  constructor(private http: HttpClient) {}

  getTopFreeApps(nr: number): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrlFree + nr);
  }

  getTopPaidApps(nr: number): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrlPaid + nr);
  }
}
