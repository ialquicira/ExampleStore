import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Products } from '../Models/Products';
import configurl from '../../assets/config/config.json'

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  url = configurl.apiServer.url + '/api/Product/';
  constructor(private http: HttpClient) { }
  getProductList(): Observable<Array<Products>> {
    var result = this.http.get<Array<Products>>(this.url + 'GetAll');
    return result;
  }
  postProductData(productData: Products): Observable<Products> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Products>(this.url + 'Create', productData, httpHeaders);
  }
  updateProduct(product: Products): Observable<Products> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Products>(this.url + 'Update?id=' + product.ProductId, product, httpHeaders);
  }
  deleteProductById(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'Delete?id=' + id, null);
  }
  getProductDetailsById(id: string): Observable<Products> {
    return this.http.get<Products>(this.url + 'GetById?id=' + id);
  }
}
