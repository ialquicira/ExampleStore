import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '../Models/Store.model';
import configurl from '../../assets/config/config.json'

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  url = configurl.apiServer.url + '/api/Store/';
  constructor(private http: HttpClient) { }
  getStoreList(): Observable<Array<Store>> {
    var result = this.http.get<Array<Store>>(this.url + 'GetAll');
    return result;
  }
  postStoreData(clientData: Store): Observable<Store> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Store>(this.url + 'Create', clientData, httpHeaders);
  }
  updateStore(client: Store): Observable<Store> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Store>(this.url + 'Update?id=' + client.StoreId, client, httpHeaders);
  }
  deleteStoreById(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'Delete?id=' + id, null);
  }
  getStoreDetailsById(id: string): Observable<Store> {
    return this.http.get<Store>(this.url + 'GetById?id=' + id);
  }
}
