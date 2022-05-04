import { CommodityModel } from "./commoditymodel.model";
import { KeyValuePair } from "./keyvaluepair.model";

export class LookupResult {
    models: KeyValuePair[] = [];
    commodities: KeyValuePair[] = [];
    tradeActions: KeyValuePair[] = [];
    commodityModels: CommodityModel[] = [];
}