import { from } from "rxjs";
import { AccountService } from "src/app/modules/account/services/account.service";

export function appInitializer(accountService: AccountService) {
    return () => from(accountService.refreshToken());
}
