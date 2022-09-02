import { Component, OnInit,OnDestroy } from '@angular/core';
import { ToastService } from '../toast.service';

@Component({
  selector: 'app-toast-alert',
  templateUrl: './toast-alert.component.html',
  styleUrls: ['./toast-alert.component.scss']
})
export class ToastAlertComponent implements OnDestroy {

  constructor(public toastService: ToastService) { }

  showStandard() {
    debugger;
    this.toastService.show('I am a standard toast');
  }

  showSuccess() {
    this.toastService.show('I am a success toast', { classname: 'bg-success text-light', delay: 10000 });
  }

  showDanger(dangerTpl:any) {
    this.toastService.show(dangerTpl, { classname: 'bg-danger text-light', delay: 15000 });
  }
  ngOnDestroy(): void {
    this.toastService.clear();
  }
}