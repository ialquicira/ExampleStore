import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from '../Models/Clients.model';
import configurl from '../../assets/config/config.json'

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  url = configurl.apiServer.url + '/api/Client/';
  constructor(private http: HttpClient) { }
  getClientList(): Observable<Array<Client>> {
    var result = this.http.get<Array<Client>>(this.url + 'GetAll');
    return result;
  }
  postClientData(clientData: Client): Observable<Client> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Client>(this.url + 'Create', clientData, httpHeaders);
  }
  updateClient(client: Client): Observable<Client> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Client>(this.url + 'Update?id=' + client.ClientId, client, httpHeaders);
  }
  deleteClientById(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'Delete?id=' + id, null);
  }
  getClientDetailsById(id: string): Observable<Client> {
    return this.http.get<Client>(this.url + 'GetById?id=' + id);
  }
}
