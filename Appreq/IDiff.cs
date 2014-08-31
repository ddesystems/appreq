using System;
using System.Collections.Generic;
using System.Text;

namespace Appreq {
  public interface IDiff<T> {
    T Diff(T other);
  }
}
