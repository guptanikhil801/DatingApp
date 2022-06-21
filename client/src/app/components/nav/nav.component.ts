import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  userName = new FormControl('');
  password = new FormControl('');

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    let data = {
      UserName: this.userName.value,
      Password: this.password.value
    }
    this.accountService.login(data).subscribe(response => {
      this.router.navigateByUrl('');
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  logOut() {
    this.accountService.logOut();
  }

}
