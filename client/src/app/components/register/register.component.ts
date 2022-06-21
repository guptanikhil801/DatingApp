import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model = { userName: '', password: '' }
  constructor(private accountService: AccountService, private toaster: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe(response => {
      this.toaster.success("Registration Successfull")
      this.cancel();
    }, error => {
      this.toaster.error(error.error);
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
