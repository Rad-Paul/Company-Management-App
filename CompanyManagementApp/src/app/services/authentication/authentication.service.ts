import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import UserForLogin from '@shared/models/users/userForLogin'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  #http = inject(HttpClient);
  apiAuthUrl : string = "https://localhost:7192/api/authentication";

  constructor(){}

  login(loginCredentials : UserForLogin){
    return this.#http.post(`${this.apiAuthUrl}/login`, loginCredentials);
  }

  logout(){
    localStorage.removeItem('token');
  }

}
