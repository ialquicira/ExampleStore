
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ClientService } from '../clients/clients.service';
import { Client } from '../Models/Clients.model';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-client',
  templateUrl: './clients.component.html'
})
export class ClientsComponent implements OnInit {

  ClientList?: Observable<Array<Client>>;
  ClientList1?: Observable<Array<Client>>;
  clientForm: any;
  massage = "";
  prodCategory = "";
  clientId = 0;
  constructor(private formbulider: FormBuilder,
     private clientService: ClientService,private router: Router,
     private jwtHelper : JwtHelperService,private toastr: ToastrService) { }

  ngOnInit() {
    this.prodCategory = "0";
    this.clientForm = this.formbulider.group({
      Name: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      Description: ['', [Validators.required]]
    });
    this.getClientList();
  }
  getClientList() {
    this.ClientList1 = this.clientService.getClientList();
    this.ClientList = this.ClientList1;
  }
  PostClient(client: Client) {
    const client_Master = this.clientForm.value;
    this.clientService.postClientData(client_Master).subscribe(
      () => {
        this.getClientList();
        this.clientForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  ClientDetailsToEdit(id: string) {
    this.clientService.getClientDetailsById(id).subscribe(clientResult => {
      this.clientId = clientResult.ClientId;
      this.clientForm.controls['Name'].setValue(clientResult.Name);
      this.clientForm.controls['LastName'].setValue(clientResult.LastName);
      this.clientForm.controls['Address'].setValue(clientResult.Address);
    });
  }
  UpdateClient(client: Client) {
    client.ClientId = this.clientId;
    const client_Master = this.clientForm.value;
    this.clientService.updateClient(client_Master).subscribe(() => {
      this.toastr.success('Data Updated Successfully');
      this.clientForm.reset();
      this.getClientList();
    });
  }

  DeleteClient(id: number) {
    if (confirm('Do you want to delete this client?')) {
      this.clientService.deleteClientById(id).subscribe(() => {
        this.toastr.success('Data Deleted Successfully');
        this.getClientList();
      });
    }
  }

  Clear(client: Client){
    this.clientForm.reset();
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
