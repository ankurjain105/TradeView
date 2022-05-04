import { CommodityModel } from "src/core/models/commoditymodel.model";
import { KeyValuePair } from "src/core/models/keyvaluepair.model";

export interface TradeActionSummary {
    quantity: number | null;
    tradeDate: Date | null;
    commodityModel?: CommodityModel | null;
    tradeAction?: KeyValuePair | null;
}

export interface TradeActionHistory {
    trades: TradeActionSummary[];
}