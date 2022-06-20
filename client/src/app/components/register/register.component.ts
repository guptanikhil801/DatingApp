import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model = { userName: '', password: '' }
  constructor() { }

  ngOnInit(): void {
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
