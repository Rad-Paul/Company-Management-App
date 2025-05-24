import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public hubConnection: signalR.HubConnection;

  constructor(){
    this.hubConnection = this.setupConnection();
    this.startConnection();
  }

  private setupConnection() : signalR.HubConnection{
    return new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7192/hubs/companyManagementHub')
      .build();
  }

  private startConnection() : void {

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
      
  }
}
