import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  userName = new FormControl('');
  password = new FormControl('');
  loggedIn: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  login() {
    let data = {
      UserName: this.userName.value,
      Password: this.password.value
    }
    this.accountService.login(data).subscribe(response => {
      this.loggedIn = true;
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  logOut() {
    this.loggedIn = false;
  }

}
