import { Component, ElementRef, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslocoModule } from '@jsverse/transloco';
import { UsuarioService } from '@services/usuario.service';
import { tap } from 'rxjs';
import { AccountState } from '@state/account.state';
import { PrimeNGConfig } from 'primeng/api';
import { SolicitudService } from '@services/solicitud.service';
import { CalendarioService } from '@services/calendario.service';
import { CalendarioState } from '@state/calendario.state';
export {};
declare global {
   interface Window {
      Calendly: any;
   }
}

@Component({
    selector: 'app-calendarioasesorado',
    // templateUrl: './calendarioasesorado.component.html',
    template: `
        <div class="card" *transloco="let t">
            <div [hidden]="calendarioMissing" class="text-900 text-3xl font-medium mb-3">{{ t('mensajes.calendarioMissing') }}</div>
            <div #container class="calendly-inline-widget" style="min-width:320px;height:700px;" data-auto-load="false"></div>
        </div>
    `,
    standalone: true,
    imports: [
        TranslocoModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        InputTextareaModule,
        InputTextModule,
        InputNumberModule,
        ButtonModule,
        RippleModule,
        FormsModule
    ]
})

export class CalendarioAsesoradoComponent implements OnInit {
    calendarioService = inject(CalendarioService);
    accountState = inject(AccountState);
    calendarioState = inject(CalendarioState);
    calendarioMissing = false;
    @ViewChild('container') container!: ElementRef;
    private waitForElm(selector: any) {
        return new Promise(resolve => {
            if (document.querySelector(selector)) {
                return resolve(document.querySelector(selector));
            }
    
            const observer = new MutationObserver(mutations => {
                if (document.querySelector(selector)) {
                    observer.disconnect();
                    resolve(document.querySelector(selector));
                }
            });
    
            // If you get "parameter 1 is not of type 'Node'" error, see https://stackoverflow.com/a/77855838/492336
            observer.observe(document.body, {
                childList: true,
                subtree: true
            });
        });
    }

    ngOnInit() {
        this.waitForElm('.calendly-inline-widget').then((elm: any) => {
            var userAccount = this.accountState.getState();
            this.calendarioState.stateItem$.subscribe({
                next: (res) => {
                    if (res) {
                        this.calendarioMissing = true;
                        window.Calendly.initInlineWidget({
                            url: res.schedulingUrl,
                            prefill: {
                                name: userAccount?.fullName,
                                email: userAccount?.email,
                            },
                            parentElement: document.querySelector('.calendly-inline-widget'),
                        });                    
                    }
                },
            })
        });
    }
}
