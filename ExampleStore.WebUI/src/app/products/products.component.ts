
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProductsService } from '../products/products.service';
import { Products } from '../Models/Products';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  ProductList?: Observable<Array<Products>>;
  ProductList1?: Observable<Array<Products>>;
  productForm: any;
  massage = "";
  prodCategory = "";
  productId = 0;
  constructor(private formbulider: FormBuilder,
     private productService: ProductsService,private router: Router,
     private jwtHelper : JwtHelperService,private toastr: ToastrService) { }

  ngOnInit() {
    this.prodCategory = "0";
    this.productForm = this.formbulider.group({
      Name: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      Description: ['', [Validators.required]]
    });
    this.getProductList();
  }
  getProductList() {
    this.ProductList1 = this.productService.getProductList();
    this.ProductList = this.ProductList1;
  }
  PostProduct(product: Products) {
    const product_Master = this.productForm.value;
    this.productService.postProductData(product_Master).subscribe(
      () => {
        this.getProductList();
        this.productForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  ProductDetailsToEdit(id: string) {
    this.productService.getProductDetailsById(id).subscribe(productResult => {
      this.productId = productResult.ProductId;
      this.productForm.controls['Name'].setValue(productResult.Name);
      this.productForm.controls['Price'].setValue(productResult.Price);
      this.productForm.controls['Description'].setValue(productResult.Description);
      this.productForm.controls['Stock'].setValue(productResult.Stock);
    });
  }
  UpdateProduct(product: Products) {
    product.ProductId = this.productId;
    const product_Master = this.productForm.value;
    this.productService.updateProduct(product_Master).subscribe(() => {
      this.toastr.success('Data Updated Successfully');
      this.productForm.reset();
      this.getProductList();
    });
  }

  DeleteProduct(id: number) {
    if (confirm('Do you want to delete this product?')) {
      this.productService.deleteProductById(id).subscribe(() => {
        this.toastr.success('Data Deleted Successfully');
        this.getProductList();
      });
    }
  }

  Clear(product: Products){
    this.productForm.reset();
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/"]);
  }

  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

}
