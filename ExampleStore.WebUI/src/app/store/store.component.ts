
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { StoreService } from '../store/store.service';
import { Store } from '../Models/Store.model';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html'
})
export class StoresComponent implements OnInit {

  StoreList?: Observable<Array<Store>>;
  StoreList1?: Observable<Array<Store>>;
  storeForm: any;
  massage = "";
  prodCategory = "";
  storeId = 0;
  constructor(private formbulider: FormBuilder,
     private storeService: StoreService,private router: Router,
     private jwtHelper : JwtHelperService,private toastr: ToastrService) { }

  ngOnInit() {
    this.prodCategory = "0";
    this.storeForm = this.formbulider.group({
      Name: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      Description: ['', [Validators.required]]
    });
    this.getStoreList();
  }
  getStoreList() {
    this.StoreList1 = this.storeService.getStoreList();
    this.StoreList = this.StoreList1;
  }
  PostStore(store: Store) {
    const store_Master = this.storeForm.value;
    this.storeService.postStoreData(store_Master).subscribe(
      () => {
        this.getStoreList();
        this.storeForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  StoreDetailsToEdit(id: string) {
    this.storeService.getStoreDetailsById(id).subscribe(storeResult => {
      this.storeId = storeResult.StoreId;
      this.storeForm.controls['StoreName'].setValue(storeResult.StoreName);
      this.storeForm.controls['Address'].setValue(storeResult.Address);
    });
  }
  UpdateStore(store: Store) {
    store.StoreId = this.storeId;
    const store_Master = this.storeForm.value;
    this.storeService.updateStore(store_Master).subscribe(() => {
      this.toastr.success('Data Updated Successfully');
      this.storeForm.reset();
      this.getStoreList();
    });
  }

  DeleteStore(id: number) {
    if (confirm('Do you want to delete this store?')) {
      this.storeService.deleteStoreById(id).subscribe(() => {
        this.toastr.success('Data Deleted Successfully');
        this.getStoreList();
      });
    }
  }

  Clear(store: Store){
    this.storeForm.reset();
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
