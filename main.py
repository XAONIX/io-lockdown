from time import sleep

import utils.active_window as aw
import utils.locked_state as ls
import utils.win_registry as wr
import utils.network as ntw

#wr.registry_read()
#aw.get_active_window()
#m = ls.WTSMonitor()
#m.start()

ntw.disable_ethernet()
sleep(5)
ntw.enable_ethernet()
