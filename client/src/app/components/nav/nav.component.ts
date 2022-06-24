import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  userName = new FormControl('');
  password = new FormControl('');

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login() {
    let data = {
      UserName: this.userName.value,
      Password: this.password.value
    }
    this.accountService.login(data).subscribe(response => {
      this.toastr.success("Login Successfull");
      this.router.navigateByUrl('');
    });
  }

  logOut() {
    this.accountService.logOut();
    this.toastr.info("Logged Out")
  }

}
