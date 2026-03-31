import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Admin {
  private API = "http://localhost:5262/api/auth";

  constructor(private http: HttpClient) {}

  createTournament(name: string) {

  return this.http.post(
    `${this.API}/tournaments`,
    { name }
  );

}

createMatch(data: any) {

  return this.http.post(
    `${this.API}/matches`,
    data
  );

}
}
