import { APP_INITIALIZER, NgModule } from "@angular/core";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from "./app.component";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http';
import { LoginComponent } from "./routes/auth/login/login.component";
import { SignupComponent } from "./routes/auth/signup/signup.component";
import { HomeComponent } from "./routes/home/home.component";
import { SolicitarComponent } from "./routes/solicitar/solicitar.component";
import { AppLayoutModule } from "./layout/app.layout.module";
import { TranslocoRootModule } from './transloco-root.module';
import { AuthState } from "@state/auth.state";
import { authInterceptor } from "./core/auth.interceptor";
import { LogoutComponent } from "./routes/auth/logout/logout.component";
import { UsuariosComponent } from "./routes/usuarios/usuarios.component";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RippleModule } from "primeng/ripple";
import { FacultadesComponent } from "./routes/facultades/facultades.component";

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        RippleModule,
        TranslocoRootModule,
        LoginComponent,
        SignupComponent,
        LogoutComponent,
        UsuariosComponent,
        HomeComponent,
        SolicitarComponent,
        FacultadesComponent
    ],
    providers: [
        provideHttpClient(withInterceptors([authInterceptor])),
        {
            provide: APP_INITIALIZER,
            // dummy factory
            useFactory: () => () => { },
            multi: true,
            // injected depdencies, this will be constructed immidiately
            deps: [AuthState],
        }
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
