import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);//account service store the auth token
  baseUrl = environment.apiUrl; //proper way to do this ep 97

  // constructor() { } dont need constructor because we using function version of injection private http = inject(HttpClient);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions());
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseUrl + 'users/' + username, this.getHttpOptions());
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.accountService.currentUser()?.token}`
      })
    }
  }
}
